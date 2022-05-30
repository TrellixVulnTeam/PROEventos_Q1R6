import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, take } from 'rxjs';
import { Lote } from '../Models/Lote';

@Injectable()
export class LoteService {

  baseURL = 'https://localhost:44388/api/Lotes';

  constructor(private http: HttpClient) { }

  public getLotesByEventoId(eventoID: number): Observable<Lote[]>{
    return this.http.get<Lote[]>(`${this.baseURL}/${eventoID}`).pipe(take(1));
  }
  public saveLote(eventoID: number, lotes: Lote[]): Observable<Lote>{
    return this.http.put<Lote>(`${this.baseURL}/${eventoID}`, lotes);
  }
  public deleteLote(loteId: number, eventoID: number): Observable<any>{
    return this.http.delete(`${this.baseURL}/${eventoID}/${loteId}`).pipe(take(1));
  }
}
