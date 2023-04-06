import { NgModule } from "@angular/core";
import { AppSharedModule } from "@app/shared/app-shared.module";
import { SubheaderModule } from "@app/shared/common/sub-header/subheader.module";
import { POGeneralTermsRoutingModule } from "./po-general-terms-routing.module";
import { POGeneralTermsComponent } from "./po-general-term-list/po-general-terms.component";
import { CreateOrEditPOGeneralTermModalComponent } from "./create-edit-po-general-term/create-or-edit-po-general-term-modal.component";

@NgModule({
    declarations: [
        POGeneralTermsComponent,
        CreateOrEditPOGeneralTermModalComponent
    ],
    imports: [
        AppSharedModule,  
        POGeneralTermsRoutingModule,
        SubheaderModule,
    ],
    providers: [],
})

export class POGeneralTermsModule {}