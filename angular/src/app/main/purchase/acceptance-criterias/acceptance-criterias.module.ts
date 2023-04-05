import { NgModule } from "@angular/core";
import { AppSharedModule } from "@app/shared/app-shared.module";
import { SubheaderModule } from "@app/shared/common/sub-header/subheader.module";
import { AcceptanceCriteriasRoutingModule } from "./acceptance-criterias-routing.module";

@NgModule({
    declarations: [
        
    ],
    imports: [
        AppSharedModule,  
        AcceptanceCriteriasRoutingModule,
        SubheaderModule,
    ],
    providers: [],
})

export class AcceptanceCriteriasModule {}