import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Evento } from '../_model/Evento';

@Injectable({
  providedIn: 'root'
})
export class EventoService {

  baseURL = 'http://localhost:5000/api/evento';

  constructor(private http: HttpClient) { }

  getAllEvento(): Observable<Evento[]> {
     return this.http.get<Evento[]>(this.baseURL);
  }

  getEventoByTema(tema: string): Observable<Evento[]> {
    return this.http.get<Evento[]>('${this.baseURL}/getByTema/${tema}');
  }

  getEventoById(id: number): Observable<Evento> {
    return this.http.get<Evento>('${this.baseURL}/${id}');
  }

  postEvento(evento: Evento) {
    return this.http.post(this.baseURL, evento);
  }

  putEvento(evento: Evento) {
    return this.http.put(this.baseURL+'/'+evento.eventoId, evento);
  }

  deleteEvento(eventoId: number) {
    return this.http.delete(this.baseURL+'/'+eventoId);
  }

  upload(file: File) {

    // pegando o arquivo enviado
    const fileToUpload = <File> file[0];

    // criando um FormData() para enviar o arq ao backend
    const formData = new FormData();

    // colocando o arquivo no formData:
    formData.append('file', fileToUpload, fileToUpload.name)

    return this.http.post(this.baseURL+'/upload', formData);
  }

}
