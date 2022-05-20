import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { PaginatedResult, Pagination } from '@app/Models/Pagination';
import { environment } from '@environments/environment';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { debounceTime, Subject } from 'rxjs';
import { Evento } from 'src/app/Models/Evento';
import { EventoService } from 'src/app/services/evento.service';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.css']
})
export class EventoListaComponent implements OnInit {

  modalRef = {} as BsModalRef;
  public eventos: any = [];
  public eventoId!: number;
  public pagination = {} as Pagination;


  public widthImg: number = 150;
  public marginImg: number = 2;
  public mostrarImg: boolean = true;

  termoBuscaChanged: Subject<string> = new Subject<string>();

  public filtrarEventos(evt: any): void {
    if(this.termoBuscaChanged.observers.length == 0) {
    this.termoBuscaChanged.pipe(debounceTime(1000)).subscribe(
      filtrarPor => {
        this.spinner.show();
        this.eventoService.getEvento(this.pagination.currentPage, this.pagination.itemsPerPage, evt.value)
        .subscribe(
          (response: PaginatedResult<Evento[]>) => {
            this.eventos = response.result;
            this.pagination = response.pagination;
          },
          (error: any) =>{
            this.spinner.hide();
            this.toastr.error('Erro ao carregar os eventos','Erro');
          }
        ).add(() => this.spinner.hide());
      });
  }
  this.termoBuscaChanged.next(evt.value);
}

  constructor(private eventoService: EventoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
    ){}

  public ngOnInit(): void {
    this.pagination = {currentPage: 1, itemsPerPage: 3, totalItems: 1} as Pagination;
    this.carregarEventos();
  }

  public alterarImg(): void {
  this.mostrarImg = !this.mostrarImg;
}

public mostraImage(imagemURL: string): string {
  return (imagemURL != '') ? `${environment.apiURL}resources/images/${imagemURL}` : 'assets/semFoto.jpg';
}

  public carregarEventos(): void
  {
    this.spinner.show();
    this.eventoService.getEvento(this.pagination.currentPage, this.pagination.itemsPerPage).subscribe(
        (response: PaginatedResult<Evento[]>) => {
        this.eventos = response.result;
        this.pagination = response.pagination;
      },
      (error: any) =>{
        this.spinner.hide();
        this.toastr.error('Erro ao carregar os eventos','Erro');
      }
    ).add(() => this.spinner.hide());
  }

  openModal(event: any, template: TemplateRef<any>, eventoId: number): void {
    event.stopPropagation();
    this.eventoId = eventoId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  public pageChanged(event): void {
    this.pagination.currentPage = event.page;
    this.carregarEventos();
  }

  confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.eventoService.deleteEvento(this.eventoId).subscribe(
      (result: any) => {if(result.message == 'Deletado') this.toastr.success("Excluido com Sucesso", "Deletado");
                                                    this.spinner.hide();
                                                    this.carregarEventos();},
      (error: any) => {console.error(error); this.toastr.error(`Erro ao tentar deletar o Evento ${this.eventoId}`, 'Erro!'); this.spinner.hide();},
      () => {this.spinner.hide();}
    );

  }

  decline(): void {
    this.modalRef?.hide();
  }

  detalheEvento(id: number): void {
    this.router.navigate([`eventos/detalhe/${id}`]);
  }
}
