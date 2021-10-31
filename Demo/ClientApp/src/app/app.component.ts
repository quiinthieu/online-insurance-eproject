import {
    AfterViewInit,
    Component,
    ElementRef, OnInit
} from "@angular/core";
import {
    // import as RouterEvent to avoid confusion with the DOM Event
    Event as RouterEvent,
    NavigationCancel,
    NavigationEnd,
    NavigationError,
    NavigationStart,
    Router
} from "@angular/router";
import * as $ from "jquery";

@Component({
    selector: "app-root",
    templateUrl: "./app.component.html",
})
export class AppComponent implements OnInit, AfterViewInit {
    title = "app";
    observer;
    loading = true;

    constructor(private elRef: ElementRef, private router: Router) {
        this.router.events.subscribe((e: RouterEvent) => {
            this.navigationInterceptor(e);
        });
    }
    // Shows and hides the loading spinner during RouterEvent changes

    navigationInterceptor(event: RouterEvent): void {
        let me = this;
        let TIME_OUT = 500
        if (event instanceof NavigationStart) {
            this.loading = true;
        }
        if (event instanceof NavigationEnd) {
            setTimeout(function () {
                me.loading = false;
            }, TIME_OUT);
        }

        // Set loading state to false in both of the below events to hide the spinner in case a request fails
        if (event instanceof NavigationCancel) {
            setTimeout(function () {
                me.loading = false;
            }, TIME_OUT);
        }
        if (event instanceof NavigationError) {
            setTimeout(function () {
                me.loading = false;
            }, TIME_OUT);
        }
    }

    ngOnInit() {
        $.getScript("../assets/js/custom/javaScript.js"); //Add path to your custom js file
    }

    ngAfterViewInit() {
        this.observer = new MutationObserver((mutations) => {
            console.log("Dom change detected...");
            $.getScript("../assets/js/custom/javaScript.js"); //Add path to your custom js file
        });
        var config = { attributes: true, childList: true, characterData: true };

        this.observer.observe(this.elRef.nativeElement, config);
    }
}
