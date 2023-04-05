import { NgModule } from "@angular/core";
import { AppSharedModule } from "@app/shared/app-shared.module";
import { SubheaderModule } from "@app/shared/common/sub-header/subheader.module";
import { POGeneralTermsRoutingModule } from "./po-general-terms-routing.module";

@NgModule({
    declarations: [
    ],
    imports: [
        AppSharedModule,  
        POGeneralTermsRoutingModule,
        SubheaderModule,
    ],
    providers: [],
})

export class POGeneralTermsModule {}