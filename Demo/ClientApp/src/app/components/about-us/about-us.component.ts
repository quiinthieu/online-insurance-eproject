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
        // this.loadScript();
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

        // Hide Information Card
        $('#about-us-page .our-team .item .team-card button').on('click', function () {
            $(this).parents('.card-c').fadeOut();
            // remove overflow hidden to html
            if ($('html').hasClass('overflow-h') == true) {
                $('html').removeClass('overflow-h');
            } else {
                return false;
            }
        });


        // Map Section
        $("#about-us-page .flag").on('click', function () {
            $('.flag').removeClass('open');
            $(this).addClass('open');
        });
        $(document).mouseup(function (e:any) {
            var flag_con = $(".flag");
            // if the target of the click isn't the container nor a descendant of the container
            if (!flag_con.is(e.target) && flag_con.has(e.target).length === 0) {
                flag_con.removeClass('open');
            }
        });
    }


}
