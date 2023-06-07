import { NgModule } from "@angular/core";
import { AppSharedModule } from "@app/shared/app-shared.module";
import { SubheaderModule } from "@app/shared/common/sub-header/subheader.module";
import { AssemblyMastersRoutingModule } from "./assembly-masters-routing.module";
import { ReactiveFormsModule } from "@angular/forms";
import { MultiSelectModule } from 'primeng/multiselect';
import { AssemblyMasterListComponent } from "./assembly-master-list/assembly-master-list.component";
import { CreateOrEditAssemblyMasterModalComponent } from "./create-edit-assembly-master/create-or-edit-assembly-master-modal.component";
import { ItemMockService } from "@app/shared/common/mock-data-services/item.mock.service";

@NgModule({
    declarations: [
        AssemblyMasterListComponent,
        CreateOrEditAssemblyMasterModalComponent
    ],
    imports: [
        AppSharedModule,  
        AssemblyMastersRoutingModule,
        MultiSelectModule,
        ReactiveFormsModule,
        SubheaderModule,
    ],
    providers: [ItemMockService],
})

export class AssemblyMastersModule {}