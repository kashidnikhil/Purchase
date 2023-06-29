import { NgModule } from "@angular/core";
import { AppSharedModule } from "@app/shared/app-shared.module";
import { SubheaderModule } from "@app/shared/common/sub-header/subheader.module";
import { TransactionsRoutingModule } from "./transactions-routing.module";
import { MaterialRequisitionsComponent } from "./material-requisitions/material-requisition-list/material-requisitions.component";

@NgModule({
    declarations: [
        MaterialRequisitionsComponent
    ],
    imports: [
        AppSharedModule,  
        TransactionsRoutingModule,
        SubheaderModule,
    ],
    providers: [],
})

export class TransactionsModule {}