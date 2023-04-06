import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MaterialGradesComponent } from './material-grade-list/material-grades.component';
const routes: Routes = [
    {
        path: '',
        component: MaterialGradesComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class MaterialGradesRoutingModule {}
