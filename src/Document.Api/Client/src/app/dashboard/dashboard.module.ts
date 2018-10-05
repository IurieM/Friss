import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { DashboardComponent } from './dashboard.component';
import { MenuComponent } from './menu.component';
import { DashboardRoutingModule } from './dashboard-routing.module';

@NgModule({
    imports: [
        SharedModule,
        DashboardRoutingModule
    ],
    declarations: [
        DashboardComponent,
        MenuComponent,
    ],
    exports: [
        SharedModule,
        DashboardComponent,
        MenuComponent,
        DashboardRoutingModule
    ]

})
export class DashboardModule { }
