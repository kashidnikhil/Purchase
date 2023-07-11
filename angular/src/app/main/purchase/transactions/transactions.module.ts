import { NgModule } from "@angular/core";
import { AppSharedModule } from "@app/shared/app-shared.module";
import { SubheaderModule } from "@app/shared/common/sub-header/subheader.module";
import { TransactionsRoutingModule } from "./transactions-routing.module";
import { MaterialRequisitionsComponent } from "./material-requisitions/material-requisition-list/material-requisitions.component";
import { CreateOrEditMaterialRequisitionModalComponent } from "./material-requisitions/create-edit-material-requisition/create-or-edit-material-requisition-modal.component";
import { ReactiveFormsModule } from "@angular/forms";
import { MaterialRequisitionMockService } from "@app/shared/common/mock-data-services/material-requisition.mock.service";

@NgModule({
    declarations: [
        MaterialRequisitionsComponent,
        CreateOrEditMaterialRequisitionModalComponent
    ],
    imports: [
        AppSharedModule,  
        TransactionsRoutingModule,
        ReactiveFormsModule,
        SubheaderModule,
    ],
    providers: [MaterialRequisitionMockService],
})

export class TransactionsModule {}