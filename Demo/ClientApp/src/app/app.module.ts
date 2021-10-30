import {BrowserModule} from '@angular/platform-browser';
import {NgModule, OnInit} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './components/nav-menu/nav-menu.component';
import {HomeComponent} from './components/home/home.component';
import {CounterComponent} from './components/counter/counter.component';
import {FetchDataComponent} from './components/fetch-data/fetch-data.component';
import {RoleComponent} from "./components/role/role.component";
import {RoleService} from "./services/role.service";
import {AboutUsComponent} from "./components/about-us/about-us.component";
import {ContactUsComponent} from "./components/contact-us/contact-us.component";
import { AppRoutingModule } from './app-routing.module';

import {initJquery} from '../assets/js/custom/javaScript';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        CounterComponent,
        FetchDataComponent,
        RoleComponent,
        AboutUsComponent,
        ContactUsComponent
    ],
    imports: [
        BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
        HttpClientModule,
        FormsModule,
        AppRoutingModule
    ],
    providers: [
        RoleService
    ],
    bootstrap: [AppComponent]
})
export class AppModule implements OnInit {
    ngOnInit(): void {

        initJquery();
    }



}
