import { NgModule } from "@angular/core";
import { AppSharedModule } from "@app/shared/app-shared.module";
import { SubheaderModule } from "@app/shared/common/sub-header/subheader.module";
import { MaterialGradesRoutingModule } from "./material-grades-routing.module";

@NgModule({
    declarations: [
    ],
    imports: [
        AppSharedModule,  
        MaterialGradesRoutingModule,
        SubheaderModule,
    ],
    providers: [],
})

export class MaterialGradesModule {}