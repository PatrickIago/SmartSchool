import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { Aluno } from '../models/Aluno';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../models/Pagination';

@Injectable({
  providedIn: 'root'
})
export class AlunoService {

  baseURL = `${environment.mainUrlAPI}aluno`;

  constructor(private http: HttpClient) { }

  getAll(page?: number, itemsPerPage?: number): Observable<PaginatedResult<Aluno[]>> {
    const paginatedResult: PaginatedResult<Aluno[]> = new PaginatedResult<Aluno[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append("pageNumber", page.toString());
      params = params.append("pageSize", itemsPerPage.toString());
    }

    return this.http.get<Aluno[]>(this.baseURL, { observe: 'response', params })
      .pipe(
        map(response => {
          paginatedResult.result = response.body || [];

          const paginationHeader = response.headers.get('Pagination');

          if (paginationHeader != null) {
            const parsed = JSON.parse(paginationHeader);
            
            paginatedResult.pagination = {
              currentPage: parsed.currentPage ?? parsed.CurrentPage,             
              itemsPerPage: parsed.itemPerPage ?? parsed.itemsPerPage ?? parsed.ItemPerPage ?? parsed.ItemsPerPage,        
              totalItems: parsed.totalItems ?? parsed.totalCount ?? parsed.TotalItems ?? parsed.TotalCount,
              totalPages: parsed.totalPages ?? parsed.TotalPages
            };
          }
          
          return paginatedResult;
        })
      );
  }

  getById(id: number): Observable<Aluno> {
    return this.http.get<Aluno>(`${this.baseURL}/${id}`);
  }

  getByDisciplinaId(id: number): Observable<Aluno[]> {
    return this.http.get<Aluno[]>(`${this.baseURL}/ByDisciplina/${id}`);
  }

  post(aluno: Aluno): Observable<any> {
    return this.http.post(this.baseURL, aluno);
  }

  put(aluno: Aluno): Observable<any> {
    return this.http.put(`${this.baseURL}/${aluno.id}`, aluno);
  }

  trocarEstado(alunoId: number, ativo: boolean): Observable<any> {
    return this.http.patch(`${this.baseURL}/${alunoId}/trocarEstado`, { estado: ativo });
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.baseURL}/${id}`);
  }
}