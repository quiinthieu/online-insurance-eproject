import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Role} from "../models/role.model";

@Injectable({
    providedIn: 'root'
})
export class RoleService {
    url: string;

    constructor(private http : HttpClient, @Inject('BASE_URL') baseUrl : string) {
        this.url = baseUrl + 'role';
    }

    findAll() {
        return this.http.get(`${this.url}/find-all`).toPromise().then(result => {
            return result as Role[];
        }, error => console.log(error));
    }

    findById(id: number) {
        return this.http.get(`${this.url}/find-by-id/${id}`).toPromise().then(result => {
            return result as Role;
        }, error => console.log(error));
    }
}
