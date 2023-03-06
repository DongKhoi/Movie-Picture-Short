import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ApiUrlConstants } from "../common/api-url.constants";

@Injectable({
    providedIn: 'root'
})
export class MovieService {
    constructor(private http: HttpClient) {
    }

    getRandomMovie() :Observable<any[]>
    {
        return this.http.get<any[]>(ApiUrlConstants.API_URL + 'movies');
    }

    getDetailMovie(id:string) :Observable<any[]>
    {
        return this.http.get<any[]>(ApiUrlConstants.API_URL + 'movies/'+ id);
    }

    getReaction(movieId: string, userId: string) :Observable<any[]>
    {
        return this.http.post<any[]>(ApiUrlConstants.API_URL + 'reaction/check', {movieId, userId});
    }
    
    createReaction(movieId: string, userId: string, status:number) :Observable<any[]>
    {
        return this.http.post<any[]>(ApiUrlConstants.API_URL + 'reaction', {movieId, userId, status});
    }

    removeReaction(movieId: string, userId: string, status:number) :Observable<any[]>
    {
        return this.http.post<any[]>(ApiUrlConstants.API_URL + 'reaction/remove', {movieId, userId, status});
    }
}