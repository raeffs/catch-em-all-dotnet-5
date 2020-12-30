//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.9.4.0 (NJsonSchema v10.3.1.0 (Newtonsoft.Json v12.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AuctionService {
    private readonly http: HttpClient;

    constructor(http: HttpClient) {
        this.http = http;
    }

    /**
     * @return Success
     */
    public getAllAuctions(): Observable<AuctionSummary[]> {
        let url = '/api/auctions';
        url = url.replace(/[?&]$/, '');

        return this.http.get<AuctionSummary[]>(url);
    }

}

@Injectable({
    providedIn: 'root'
})
export class SearchQueryService {
    private readonly http: HttpClient;

    constructor(http: HttpClient) {
        this.http = http;
    }

    /**
     * @param pageNumber (optional) 
     * @param pageSize (optional) 
     * @return Success
     */
    public getAllSearchQueries(pageNumber?: number | null | undefined, pageSize?: number | null | undefined): Observable<SearchQuerySummaryPage> {
        let url = '/api/search-queries?';
        if (pageNumber != null)
            url += "pageNumber=" + encodeURIComponent("" + pageNumber) + "&";
        if (pageSize != null)
            url += "pageSize=" + encodeURIComponent("" + pageSize) + "&";
        url = url.replace(/[?&]$/, '');

        return this.http.get<SearchQuerySummaryPage>(url);
    }

    /**
     * @return Success
     */
    public createSearchQuery(body: CreateSearchQueryOptions): Observable<SearchQueryDetail> {
        let url = '/api/search-queries';
        url = url.replace(/[?&]$/, '');

        const _body = body;

        return this.http.post<SearchQueryDetail>(url, _body);
    }

    /**
     * @return Success
     */
    public deleteSearchQuery(id: string): Observable<void> {
        let url = '/api/search-queries?';
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined and cannot be null.");
        else
            url += "id=" + encodeURIComponent("" + id) + "&";
        url = url.replace(/[?&]$/, '');

        return this.http.delete<void>(url);
    }

    /**
     * @return Success
     */
    public getSearchQuery(id: string): Observable<SearchQueryDetail> {
        let url = '/api/search-queries/{id}';
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url = url.replace("{id}", encodeURIComponent("" + id));
        url = url.replace(/[?&]$/, '');

        return this.http.get<SearchQueryDetail>(url);
    }

    /**
     * @return Success
     */
    public updateSearchQuery(id: string, body: SearchQueryDetail): Observable<SearchQueryDetail> {
        let url = '/api/search-queries/{id}';
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url = url.replace("{id}", encodeURIComponent("" + id));
        url = url.replace(/[?&]$/, '');

        const _body = body;

        return this.http.put<SearchQueryDetail>(url, _body);
    }

}

@Injectable({
    providedIn: 'root'
})
export class SearchResultService {
    private readonly http: HttpClient;

    constructor(http: HttpClient) {
        this.http = http;
    }

    /**
     * @return Success
     */
    public getAllResults(queryId: string): Observable<SearchResultSummary[]> {
        let url = '/api/search-queries/{queryId}/search-results';
        if (queryId === undefined || queryId === null)
            throw new Error("The parameter 'queryId' must be defined.");
        url = url.replace("{queryId}", encodeURIComponent("" + queryId));
        url = url.replace(/[?&]$/, '');

        return this.http.get<SearchResultSummary[]>(url);
    }

    /**
     * @return Success
     */
    public deleteResult(queryId: string, id: string): Observable<void> {
        let url = '/api/search-queries/{queryId}/search-results/{id}';
        if (queryId === undefined || queryId === null)
            throw new Error("The parameter 'queryId' must be defined.");
        url = url.replace("{queryId}", encodeURIComponent("" + queryId));
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url = url.replace("{id}", encodeURIComponent("" + id));
        url = url.replace(/[?&]$/, '');

        return this.http.delete<void>(url);
    }

}

export interface AuctionSummary {
    id?: string;
    name?: string | null;
    ends?: string;
    bidPrice?: number | null;
    purchasePrice?: number | null;
}

export type Priority = 0 | 1 | 2;

export interface SearchQuerySummary {
    id: string;
    name: string;
    priority: Priority;
    updated: string;
    numberOfAuctions: number;
}

export interface SearchQuerySummaryPage {
    items: SearchQuerySummary[];
    totalItems: number;
    pageSize: number;
    pageNumber: number;
}

export interface CreateSearchQueryOptions {
    searchTerm?: string | null;
}

export interface SearchCriteria {
    withAllTheseWords?: string | null;
    withOneOfTheseWords?: string | null;
    withExactlyTheseWords?: string | null;
    withNoneOfTheseWords?: string | null;
}

export interface SearchQueryDetail {
    id?: string;
    name?: string | null;
    criteria?: SearchCriteria;
}

export interface SearchResultSummary {
    id: string;
    queryId: string;
    name: string;
    ends: string;
    bidPrice?: number | null;
    purchasePrice?: number | null;
    updated: string;
}