import {Component, OnInit} from '@angular/core';
import * as $ from 'jquery';


@Component({
    templateUrl: './about-us.component.html',
    styleUrls: ['./about-us.component.css']
})
export class AboutUsComponent implements OnInit {

    constructor() {
    }

    ngOnInit() {
        this.loadScript();
    }

    loadScript() {
        // -- :: About Us Page
        // Hide Information card
        $('#about-us-page .our-team .item .card-c').fadeOut(0);

        // Show Information Card
        $('#about-us-page .our-team .item > span, .our-team .item > img').on('click', function () {
            $(this).parents('.item').find('.card-c').fadeIn();
            // add overflow hidden to html
            if ($('html').hasClass('overflow-h') !== true) {
                $('html').addClass('overflow-h');
            } else {
                return false;
            }
        });
    }



}
