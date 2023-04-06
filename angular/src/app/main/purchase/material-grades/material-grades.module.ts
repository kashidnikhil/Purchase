import { NgModule } from "@angular/core";
import { AppSharedModule } from "@app/shared/app-shared.module";
import { SubheaderModule } from "@app/shared/common/sub-header/subheader.module";
import { MaterialGradesRoutingModule } from "./material-grades-routing.module";
import { MaterialGradesComponent } from "./material-grade-list/material-grades.component";
import { CreateOrEditMaterialGradeModalComponent } from "./create-edit-material-grade/create-or-edit-material-grade-modal.component";

@NgModule({
    declarations: [
        MaterialGradesComponent,
        CreateOrEditMaterialGradeModalComponent
    ],
    imports: [
        AppSharedModule,  
        MaterialGradesRoutingModule,
        SubheaderModule,
    ],
    providers: [],
})

export class MaterialGradesModule {}