import {BrowserModule} from '@angular/platform-browser';
import {AfterViewInit, DoCheck, NgModule, OnInit} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './components/nav-menu/nav-menu.component';
import {HomeComponent} from './components/home/home.component';

import {FetchDataComponent} from './components/fetch-data/fetch-data.component';

import {RoleService} from "./services/role.service";
import {AboutUsComponent} from "./components/about-us/about-us.component";
import {ContactUsComponent} from "./components/contact-us/contact-us.component";
import { AppRoutingModule } from './app-routing.module';

import {initJquery} from '../assets/js/custom/javaScript';
import {ErrorComponent} from "./components/error/error.component";

import {HelpComponent} from "./components/help/help.component";
import {LifeInsuranceComponent} from "./components/life-insurance/life-insurance.component";
import {HealthInsuranceComponent} from "./components/health-insurance/health-insurance.component";
import {MotorInsuranceComponent} from "./components/motor-insurance/motor-insurance.component";
import {LoginComponent} from "./components/login/login.component";
import {RegisterComponent} from "./components/register/register.component";
import {PreloaderComponent} from "./elements/preloader/preloader.component";
import {NavBarComponent} from "./elements/nav-bar/nav-bar.component";
import {FooterComponent} from "./elements/footer/footer.component";
import {NewsletterBarComponent} from "./elements/newsletter-bar/newsletter-bar.component";
import {LoginFormComponent} from "./elements/login-form/login-form.component";
import {LogoContainerComponent} from "./elements/logo-container/logo-container.component";

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        FetchDataComponent,
        AboutUsComponent,
        ContactUsComponent,
        ErrorComponent,
        HelpComponent,
        LifeInsuranceComponent,
        HealthInsuranceComponent,
        MotorInsuranceComponent,
        LoginComponent,
        RegisterComponent,
        PreloaderComponent,
        NavBarComponent,
        FooterComponent,
        NewsletterBarComponent,
        LoginFormComponent,
        LogoContainerComponent
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
export class AppModule implements OnInit, AfterViewInit {
    ngOnInit(): void {
        // initJquery();
    }

    ngAfterViewInit(): void {
        // initJquery();
    }


}
