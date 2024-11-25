import { AfterViewInit, Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable, BehaviorSubject, switchMap } from 'rxjs';
import { ClienteModel } from "../../models/cliente";

import { PageResponse } from '../../models/page-response';
import { ClienteService } from '../../services/cliente.service';
import { ListOptions } from '../../models/list.options';
import { QueryFilter } from '../../models/filter';

import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NzAntdTableComponent, RowActionClickedEventArgs, SortingChangedEventArgs } from '../../components/table/nz-antd-table/nz-antd-table.component';
import { Column } from '../../models/column';
import { CompanyFiltersComponent } from "./filters/company-filters.component";
import { ClienteFormComponent, ClienteFormEventArgs } from './form/cliente-form.component';

import { NzDrawerModule, NzDrawerService, NzDrawerRef } from 'ng-zorro-antd/drawer';
import { ValueLabel } from '../../models/value-label';

@Component({
  selector: 'app-company',
  templateUrl: './cliente.component.html',
  styleUrl: './cliente.component.css',
  standalone: true,

  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NzAntdTableComponent,
    CompanyFiltersComponent,
    ClienteFormComponent,
    NzDrawerModule
  ]
})
export class clienteComponent implements OnInit
{
  columns: Column[] = [
    {
      title: "Nome do Cliente",
      field: "nome",
      width: "45%",
      sortable: true,
      filter: true,
      dataType: "string"
    },

    {
      title: "Tamanho da empresa",
      field: "empresaPorte",
      width: "15%",
      sortable: true,
      filter: true,
      dataType: "string"
    }
  ];

  clientes: any[] = [];

  currentListResponse: PageResponse<ClienteModel> = {
    data: [] as ClienteModel[],
    count: 0,
    page: 1,
    take: 10
  } as PageResponse<ClienteModel>;

  currentFilterVersion: number = 0;
  currentFilters: QueryFilter[] = [];
  currentPage: number = 1;
  currentPageSize: number = 10;
  currentEditing!: ClienteModel;

  filterVersionBehaviorSubject: BehaviorSubject<number> = new BehaviorSubject<number>(this.currentFilterVersion);
  dataObservable: Observable<PageResponse<ClienteModel>> = new Observable<PageResponse<ClienteModel>>();
  data: ClienteModel[] = [];
  isFormOpened: boolean = false;
  isTableLoading: boolean = false;

  get totalPages(): number {
    return Math.ceil((this.currentListResponse?.count ?? 0) / this.currentPageSize);
  }

  constructor(
    public drawerService: NzDrawerService,
    private clienteService: ClienteService
  ) {

  }

  loadData(): void {
    this.isTableLoading = true;
    const version: number = this.currentFilterVersion + 1;
    this.filterVersionBehaviorSubject.next(version);
  }

  ngOnInit(): void {
    this.dataObservable = this
      .filterVersionBehaviorSubject
      .pipe(
        switchMap(
          (version: number) => {
            if(version == this.currentFilterVersion)
              return new Observable<PageResponse<ClienteModel>>(observer => observer.next(this.currentListResponse));

            this.currentFilterVersion = version;

            const options = new ListOptions();

            options.page = this.currentPage,
            options.take = this.currentPageSize,
            options.filters = this.currentFilters;

            return this.clienteService.listAll(options);
          }
        )
      );

    this.dataObservable.subscribe(response => {
      this.currentListResponse = response;
      this.data = response?.data ?? [];
      this.isTableLoading = false;
    });

    this.loadData();
  }

  getEmpresaPorteLabel(empresaPorteId: number): string {
    const mapeamento: { [key: number]: string } = {
      1: "1 - Pequeno",
      2: "2 - Médio",
      3: "3 - Grande"
    };
  
    // Retorna o rótulo correspondente ou uma mensagem padrão
    return mapeamento[empresaPorteId] || `${empresaPorteId} - Desconhecido`;
  }

  removeScrollBlockClass() : void {
    document.scrollingElement?.classList.remove('cdk-global-scrollblock');
  }

  openForm(editing: boolean): void {
    if (!editing)
      this.currentEditing = {};

    this.isFormOpened = true;
  }

  drawerClosed(e: MouseEvent) : void {
    this.isFormOpened = false;
    this.removeScrollBlockClass();
  }

  drawerFormCanceled(e: ClienteFormEventArgs) : void {
    this.isFormOpened = false;
    this.removeScrollBlockClass();
  }

  drawerFormAfterSaved(e: ClienteFormEventArgs) : void {
    this.isFormOpened = false;
    this.removeScrollBlockClass();
    this.loadData();
  }

  tableFilterChanged(args: QueryFilter[]): void {
    this.currentPage = 1;
    this.currentFilters = args;
    this.loadData();
  }

  tablePageSizeChanged(args: number): void {
    if (this.currentPageSize == args)
      return;

    this.currentPageSize = args;
    this.loadData();
  }

  tablePageChanged(args: number): void {
    if (this.currentPage == args)
      return;

    this.currentPage = args;
    this.loadData();
  }

  tableRowActionClick(args: RowActionClickedEventArgs) : void {
    switch(args.action) {
      case 'delete':
        this.isTableLoading = true;

        this.clienteService.delete(args.data.id).subscribe(response => {
          this.isTableLoading = false;
          this.loadData();
        });
        break;
      case 'edit':
        const { id, name, size} = args.data;
        this.currentEditing = { id: id, nome: name, empresaPorteId: size };
        this.openForm(true);
        break;
    }
  }
}
