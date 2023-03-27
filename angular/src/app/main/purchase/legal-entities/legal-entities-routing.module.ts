import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LegalEntitiesComponent } from './legal-entity-list/legal-entities.component';

const routes: Routes = [
    {
        path: '',
        component: LegalEntitiesComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class LegalEntitiesRoutingModule {}
