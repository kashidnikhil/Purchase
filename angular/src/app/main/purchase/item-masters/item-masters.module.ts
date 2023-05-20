import { NgModule } from "@angular/core";
import { AppSharedModule } from "@app/shared/app-shared.module";
import { SubheaderModule } from "@app/shared/common/sub-header/subheader.module";
import { ItemMastersRoutingModule } from "./item-masters-routing.module";
import { ReactiveFormsModule } from "@angular/forms";
import { MultiSelectModule } from 'primeng/multiselect';
import { ItemMasterListComponent } from "./item-master-list/item-master-list.component";
import { CreateOrEditItemMasterModalComponent } from "./create-edit-item-master/create-or-edit-item-master-modal.component";
import { ItemMockService } from "@app/shared/common/mock-data-services/item.mock.service";

@NgModule({
    declarations: [
        ItemMasterListComponent,
        CreateOrEditItemMasterModalComponent
    ],
    imports: [
        AppSharedModule,  
        ItemMastersRoutingModule,
        MultiSelectModule,
        ReactiveFormsModule,
        SubheaderModule,
    ],
    providers: [ItemMockService],
})

export class ItemMastersModule {}