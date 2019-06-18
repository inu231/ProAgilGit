import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventoService } from '../_services/Evento.service';
import { Evento } from '../_model/Evento';
import { BsModalRef, BsModalService, BsDropdownModule  } from 'ngx-bootstrap';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { defineLocale, BsLocaleService, ptBrLocale } from "ngx-bootstrap";

defineLocale('pt-br', ptBrLocale);

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {
  eventosFiltrados: Evento[];
  eventos: Evento[];
  evento: Evento;
  locais: any = [];
  imagemLargura = 50;
  imagemMargem = 50;
  mostrarImagem = false;
  modalRef: BsModalRef;
  formEvento: FormGroup;
  acao: string;
  _filtroLista: string;

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private fb: FormBuilder,
    private localeService: BsLocaleService
    ) { 
      this.localeService.use('pt-br');
    }

    get filtroLista(): string {
      return this._filtroLista;
    }
    set filtroLista(value: string) {
      this._filtroLista = value;
      this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
    }


    ngOnInit() {
      this.getEventos();
      this.validation();
    }

    alternarImagem() {
      this.mostrarImagem = !this.mostrarImagem;
    }

    getEventos() {
      this.eventoService.getAllEvento().subscribe(
        (_eventos: Evento[]) => {
          this.eventos = _eventos;
          this.eventosFiltrados = this.eventos;
          console.log(_eventos);
        },
        error => {console.log(error)}
        );
      }

    filtrarEventos(filtrarPor): Evento[] {
      filtrarPor = filtrarPor.toLocaleLowerCase();
      return this.eventos.filter(
        evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
        );
      }

    editarEvento(evento: Evento, template: any) {
      this.acao = 'UPDATE';
      this.openModal(template);
      this.evento = evento;
      this.formEvento.patchValue(this.evento);
    }

    novoEvento(template: any) {
      this.acao = 'INSERT';
      this.openModal(template);
    }

    openModal(template: any) {
      this.formEvento.reset();
      template.show();
    }

    validation() {
      this.formEvento = this.fb.group({
        tema: ['', [
          Validators.required, Validators.minLength(4), Validators.maxLength(1200)]],
          local: ['', Validators.required],
          dataEvento: ['', Validators.required],
          qtdPessoas: ['', [Validators.required, Validators.maxLength(500)]],
          telefone: ['', Validators.required],
          email: ['', [Validators.required, Validators.email]],
          imagemURL: ['', Validators.required],
        });
      }

    salvarAlteracao(modal: any) {
    if(this.formEvento.valid) {
        if(this.acao === 'INSERT') {
          this.evento = Object.assign({}, this.formEvento.value);
          this.eventoService.postEvento(this.evento).subscribe(
            (novoEvento: Evento) => {
              console.log(novoEvento);
              modal.hide();
              this.getEventos();
            },
            error => {
              console.log(error);
            }
          );
        } else if (this.acao === 'UPDATE') {
          this.evento = Object.assign({eventoId: this.evento.eventoId}, this.formEvento.value);
          console.log(this.evento);
          this.eventoService.putEvento(this.evento).subscribe(
            () => {
              modal.hide();
              this.getEventos();
            }, 
            error => {
              console.log(error)
            }
          );
        }
      }
    }

    excluirEvento(EventoId) {
      console.log('evento', EventoId);
       this.eventoService.deleteEvento(EventoId).subscribe(
        (response: any ) => {
           this.getEventos();
        }, error => {
            console.log(error);
        }
       );
    }
}