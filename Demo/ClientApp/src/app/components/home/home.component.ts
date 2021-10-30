import {Component, OnInit} from '@angular/core';
import {initJquery} from '../../../assets/js/custom/javaScript';

@Component({
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

    constructor() {
    }

    ngOnInit() {
        initJquery();
    }

}
