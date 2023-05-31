import { Injectable } from "@angular/core";
import { FormBuilder, FormGroup } from "@angular/forms";

@Injectable()
export class ItemFormBuilderService{
    constructor(
        private formBuilder: FormBuilder,
    ){

    }

    itemMasterForm!: FormGroup;



}