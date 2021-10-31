import {Component, OnInit} from '@angular/core';
import * as $ from 'jquery';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

    constructor() {
    }

    ngOnInit() {
        // this.loadScript();
    }

    loadScript() {
        // -- :: Login Page
        // Input Verification
        $('#login-page #password-btn').on('click', function () {
            // toggle eye icon
            $('#login-page #password-btn i').toggleClass('fa-eye-slash');
            var inputField = $('#password-input');
            // get the attribute value
            var type = $(inputField).attr("type");
            // now test it's value
            if (type === 'password') {
                $(inputField).attr("type", "text");
            } else {
                $(inputField).attr("type", "password");
            }
        });
        // ----------------------------------
    }

}
