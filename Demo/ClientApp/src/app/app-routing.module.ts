import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {HomeComponent} from "./components/home/home.component";
import {AboutUsComponent} from "./components/about-us/about-us.component";
import {ErrorComponent} from "./components/error/error.component";
import {LoginComponent} from "./components/login/login.component";
import {RegisterComponent} from "./components/register/register.component";
import {ContactUsComponent} from "./components/contact-us/contact-us.component";
import {LifeInsuranceComponent} from "./components/life-insurance/life-insurance.component";
import {HealthInsuranceComponent} from "./components/health-insurance/health-insurance.component";
import {MotorInsuranceComponent} from "./components/motor-insurance/motor-insurance.component";

export const routes: Routes = [
    {
        path: '',
        component: HomeComponent,
    },
    {
        path: 'about-us',
        component: AboutUsComponent
    },
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'register',
        component: RegisterComponent
    },
    {
        path: 'contact-us',
        component: ContactUsComponent
    },
    {
        path: 'life-insurance',
        component: LifeInsuranceComponent
    },
    {
        path: 'health-insurance',
        component: HealthInsuranceComponent
    },
    {
        path: 'motor-insurance',
        component: MotorInsuranceComponent
    },
    {path: '**', component: ErrorComponent},
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule {
}
