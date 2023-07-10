import { DropdownDto } from "../data-models/dropdown";

export class MaterialRequisitionMockService{
    constructor(){}

    loadLocations(): DropdownDto[] {
        let itemTypesList = [
            {
                title: "Head Office",
                value: 1
            },
            {
                title: "Factory",
                value: 2
            }       
        ];
        return itemTypesList;
    }

    loadMaterialRequisitionType(): DropdownDto[] {
        let itemTypesList = [
            {
                title: "Single Item",
                value: 1
            },
            {
                title: "Assembly Wise Item",
                value: 2
            },
            {
                title: "Model Wise Item",
                value: 3
            }         
        ];
        return itemTypesList;
    }
}