import {
  Component,
  Input,
  Output,
  EventEmitter,
  OnInit,
  OnChanges,
  SimpleChanges
}
from '@angular/core';

import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';

import {
  FormArray,
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators
}
from '@angular/forms';

import { CommonModule } from '@angular/common';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { ModalService } from '../../../services/modal.service';
import { NzMessageService } from 'ng-zorro-antd/message';
import { ClienteModel, CompanySize } from '../../../models/cliente';
import UtilsService from '../../../services/utils.service';
import { ClienteService } from '../../../services/cliente.service';
import { ValueLabel } from '../../../models/value-label';

export interface ClienteFormEventArgs
{
  data?: ClienteModel
}

@Component({
  selector: 'app-cliente-form',
  templateUrl: './cliente-form.component.html',
  styleUrl: './cliente-form.component.css',
  standalone: true,

  imports: [
    CommonModule,
    FormsModule,
    NzSelectModule,
    NzDatePickerModule,
    ReactiveFormsModule,
    NzFormModule,
    NzCardModule,
    NzButtonModule,
    NzInputNumberModule,
    NzFlexModule,
    NzInputModule
  ]
})
export class ClienteFormComponent implements OnInit, OnChanges
{
  @Input() data!: ClienteModel;
  @Output() afterSave: EventEmitter<ClienteFormEventArgs> = new EventEmitter<ClienteFormEventArgs>(true);
  @Output() canceled: EventEmitter<ClienteFormEventArgs> = new EventEmitter<ClienteFormEventArgs>(true);

  form!: FormGroup;
  formChildren!: FormArray;
  formChanged: boolean = false;
  saving: boolean = false;
  sizes: ValueLabel[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private modalService: ModalService,
    private messageService: NzMessageService,
    private clienteService: ClienteService
  ) {
    this.sizes.push(CompanySize.SMALL);
    this.sizes.push(CompanySize.MEDIUM);
    this.sizes.push(CompanySize.LARGE);

    this.form = this.formBuilder.group({
      nome: [ this.data?.nome, Validators.required ],
      empresaPorteId: [ this.data?.empresaPorteId, Validators.required ]
    });
  }

  ngOnInit(): void {
    this.form.valueChanges.subscribe(() => {
      this.formChanged = true;
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes && changes['data'] && changes['data'].currentValue)
      this.updateForm(changes['data'].currentValue);
  }

  updateForm(data: ClienteModel) : void {
    this.form.setValue({
      'nome': data?.nome ?? "",
      'size': data?.empresaPorteId ?? 0
    });
  }

  updateData() : void {
    if(!this.data)
      this.data = {};

    this.data.nome = this.form.get('name')?.value;
    this.data.empresaPorteId = +this.form.get('size')?.value;
  }

  onSubmit() {
    if (!this.form.valid) {
      UtilsService.markFormAsDirty(this.form);
      return;
    }

    this.updateData();
    this.saving = true;

    this.clienteService.save(this.data).subscribe(response => {
      this.messageService.create('success', `O item foi cadastrado com sucesso!`);
      this.saving = false;

      this.afterSave.emit({ data: this.data });
    })
  }

  onCancel() : void {
    if (!this.formChanged) {
      this.canceled.emit({});
      return;
    }

    this.modalService.cancelar().subscribe(result => {
      if (result === 'ok') {
        this.canceled.emit({});
      }
    });
  }
}
