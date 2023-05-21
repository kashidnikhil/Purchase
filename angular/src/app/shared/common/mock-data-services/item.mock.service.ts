import { DropdownDto } from "../data-models/dropdown";

export class ItemMockService{

    constructor(){}

    loadItemCategories(): DropdownDto[] {
        let itemCategoryList = [
            {
                title: "Lab Instruments",
                value: 10001
            },
            {
                title: "Office Equipments",
                value: 20001
            },
            {
                title: "R & M",
                value: 30001
            },
            {
                title: "Books",
                value: 40001
            },
            {
                title: "Glassware",
                value: 50001
            },
            {
                title: "Chemicals",
                value: 60001
            },
            {
                title: "Material",
                value: 70001
            },
            {
                title: "Furniture & Fixtures",
                value: 80001
            },
            {
                title: "Tools & Tackles",
                value: 90001
            }
        ];
    
        return itemCategoryList;
    }

    loadItemTypes(): DropdownDto[] {
        let itemTypesList = [
            {
                title: "Analogue",
                value: 1
            },
            {
                title: "Digital",
                value: 2
            }       
        ];
        return itemTypesList;
    }

    loadYesOrNoTypeDropdownData() :DropdownDto[] {
        let options = [
            {
                title: "Yes",
                value: 1
            },
            {
                title: "No",
                value: 2
            }       
        ];

        return options;
    } 

    loadAMCRequiredList(): DropdownDto[] {
        let itemTypesList = [
            {
                title: "Analogue",
                value: 1
            },
            {
                title: "Digital",
                value: 2
            }       
        ];
        return itemTypesList;
    }
}
