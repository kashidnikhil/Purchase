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

    loadItemMobilityList() : DropdownDto[] {

        let options = [
            {
                title: "Fixed",
                value: 1
            },
            {
                title: "Field",
                value: 2
            }       
        ];

        return options;
    }

    loadCalibrationTypeList() : DropdownDto[]{
        let options = [
            {
                title: "External",
                value: 1
            },
            {
                title: "Internal",
                value: 2
            },
            {
                title: "Intermediate",
                value: 3
            }      
        ];

        return options;

    }

    loadCalibrationFrequencyList() : DropdownDto[]{
        let options = [
            {
                title: "Monthly",
                value: 1
            },
            {
                title: "Yearly",
                value: 2
            }   
        ];

        return options;

    }

    loadItemStatusList() : DropdownDto[]{
        let options = [
            {
                title: "Active",
                value: 1
            },
            {
                title: "Inactive",
                value: 2
            }   
        ];

        return options;
    }

    loadSubjectCategoryList() : DropdownDto[]{
        let options = [
            {
                title: "Chemistry",
                value: 1
            },
            {
                title: "Marketing",
                value: 2
            },
            {
                title: "Drafting",
                value: 3
            },
            {
                title: "Management",
                value: 4
            },
            {
                title: "Other",
                value: 5
            }   
        ];

        return options;
    }

}
