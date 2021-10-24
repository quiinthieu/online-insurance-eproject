import {Component, Inject, OnInit} from '@angular/core';
import {Role} from "../../models/role.model";
import {RoleService} from "../../services/role.service";


@Component({
    templateUrl: './role.component.html',
    styleUrls: ['./role.component.css']
})
export class RoleComponent implements OnInit {
    public roles : Role[];
    public id: number;
    public role : Role;

    constructor(private roleService: RoleService) {
        roleService.findAll().then(result => this.roles = result as Role[], error => console.log(error));
    }

    ngOnInit() {

    }

    findById() {
        this.roleService.findById(this.id).then(result => this.role = result as Role, error => console.log(error));
    }
}

