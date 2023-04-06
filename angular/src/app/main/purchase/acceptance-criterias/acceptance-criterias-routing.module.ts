import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AcceptanceCriteriasComponent } from './acceptance-criteria-list/acceptance-criterias.component';

const routes: Routes = [
    {
        path: '',
        component: AcceptanceCriteriasComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AcceptanceCriteriasRoutingModule {}
