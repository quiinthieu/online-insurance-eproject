import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
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

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        CounterComponent,
        FetchDataComponent,
        RoleComponent
    ],
    imports: [
        BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            {path: '', component: HomeComponent, pathMatch: 'full'},
            {path: 'counter', component: CounterComponent},
            {path: 'fetch-data', component: FetchDataComponent},
            {path: 'role', component: RoleComponent}

        ])
    ],
    providers: [
        RoleService
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
}
