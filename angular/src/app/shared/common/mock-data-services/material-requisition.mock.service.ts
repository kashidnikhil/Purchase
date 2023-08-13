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
                title: "Single item",
                value: 1
            },
            {
                title: "Assembly wise item",
                value: 2
            },
            {
                title: "Model wise item",
                value: 3
            }         
        ];
        return itemTypesList;
    }
}