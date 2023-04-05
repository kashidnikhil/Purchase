import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
const routes: Routes = [
    // {
    //     path: '',
    //     component: UnitsComponent,
    //     pathMatch: 'full',
    // },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class MaterialGradesRoutingModule {}
