import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { POGeneralTermsComponent } from './po-general-term-list/po-general-terms.component';

const routes: Routes = [
    {
        path: '',
        component: POGeneralTermsComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class POGeneralTermsRoutingModule {}
