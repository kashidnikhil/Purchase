import { NgModule } from "@angular/core";
import { AppSharedModule } from "@app/shared/app-shared.module";
import { SubheaderModule } from "@app/shared/common/sub-header/subheader.module";
import { SubAssemblyItemsRoutingModule } from "./sub-assembly-items-routing.module";
import { ReactiveFormsModule } from "@angular/forms";
import { MultiSelectModule } from 'primeng/multiselect';
import { ItemMockService } from "@app/shared/common/mock-data-services/item.mock.service";
import { SubAssemblyItemListComponent } from "./sub-assembly-item-list/sub-assembly-item-list.component";
import { CreateOrEditSubAssemblyItemModalComponent } from "./create-edit-sub-assembly-item/create-or-edit-sub-assembly-item-modal.component";

@NgModule({
    declarations: [
        SubAssemblyItemListComponent,
        CreateOrEditSubAssemblyItemModalComponent
    ],
    imports: [
        AppSharedModule,  
        SubAssemblyItemsRoutingModule,
        MultiSelectModule,
        ReactiveFormsModule,
        SubheaderModule,
    ],
    providers: [ItemMockService],
})

export class SubAssemblyItemsModule {}