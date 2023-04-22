import { NgModule } from "@angular/core";
import { AppSharedModule } from "@app/shared/app-shared.module";
import { SubheaderModule } from "@app/shared/common/sub-header/subheader.module";
import { CompaniesRoutingModule } from "./companies-routing.module";
import { ReactiveFormsModule } from "@angular/forms";
import { CompanyListComponent } from "./company-list/company-list.component";
import { CreateOrEditCompanyModalComponent } from "./create-edit-company/create-or-edit-company-modal.component";

@NgModule({
    declarations: [
         CompanyListComponent,
         CreateOrEditCompanyModalComponent
    ],
    imports: [
        AppSharedModule,  
        CompaniesRoutingModule,
        ReactiveFormsModule,
        SubheaderModule,
    ],
    providers: [],
})

export class CompaniesModule {}