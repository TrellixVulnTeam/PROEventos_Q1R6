import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, take } from 'rxjs';
import { Lote } from '../Models/Lote';

@Injectable()
export class LoteService {

  baseURL = 'https://localhost:44388/api/Lotes';

  constructor(private http: HttpClient) { }

  public getLotesByEventoId(eventoId: number): Observable<Lote[]>{
    return this.http.get<Lote[]>(`${this.baseURL}/${eventoId}`).pipe(take(1));
  }
  public saveLote(eventoId: number, lotes: Lote[]): Observable<Lote>{
    return this.http.put<Lote>(`${this.baseURL}/${eventoId}`, lotes);
  }
  public deleteLote(loteId: number, eventoId: number): Observable<any>{
    return this.http.delete(`${this.baseURL}/${eventoId}/${loteId}`).pipe(take(1));
  }
}
