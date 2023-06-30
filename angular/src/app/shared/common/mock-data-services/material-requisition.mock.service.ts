import { DropdownDto } from "../data-models/dropdown";

export class MaterialRequisitionService{
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
}