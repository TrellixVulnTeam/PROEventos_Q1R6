import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environments/environment';
import { Observable, take } from 'rxjs';
import { Evento } from '../Models/Evento';

@Injectable()
export class EventoService {

  baseURL = environment.apiURL + 'api/Eventos';

constructor(private http: HttpClient) { }

public getEvento(): Observable<Evento[]>{
  return this.http.get<Evento[]>(this.baseURL).pipe(take(1));
}
public getEventosByTema(tema: string): Observable<Evento[]>{
  return this.http.get<Evento[]>(`${this.baseURL}/${tema}/tema`);
}
public getEventoById(id: number): Observable<Evento>{
  return this.http.get<Evento>(`${this.baseURL}/${id}`);
}
public post(evento: Evento): Observable<Evento>{
  return this.http.post<Evento>(this.baseURL, evento);
}
public put(evento: Evento): Observable<Evento>{
  return this.http.put<Evento>(`${this.baseURL}/${evento.id}`, evento);
}
public deleteEvento(id: number): Observable<any>{
  return this.http.delete(`${this.baseURL}/${id}`).pipe(take(1));
}

postUpload(eventoId: number, file:File):Observable<Evento> {
  const fileToUpload = file[0] as File;
  const formData = new FormData();
  formData.append('file', fileToUpload);


  return this.http.post<Evento>(`${this.baseURL}/upload-image/${eventoId}`, formData).pipe(take(1));
}
}
