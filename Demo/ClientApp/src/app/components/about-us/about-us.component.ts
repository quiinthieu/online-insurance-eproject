import {Component, OnInit} from '@angular/core';
import {initJquery} from '../../../assets/js/custom/javaScript';
import {init} from "protractor/built/launcher";

@Component({
    templateUrl: './about-us.component.html',
    styleUrls: ['./about-us.component.css']
})
export class AboutUsComponent implements OnInit {

    constructor() {
    }

    ngOnInit() {
        initJquery();
    }

}
