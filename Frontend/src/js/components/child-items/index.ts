import axios from "src/js/axios";
import Vue from "vue";
import Component from "vue-class-component";
import { Prop, Watch } from "vue-property-decorator";
import queryString from 'querystring';
import Pager from "src/js/models/pager";
import { MaxValueItem } from "src/js/models/item";

@Component({
    template: require('./template.html')
})
export default class ChildItemsComponent extends Vue{
    @Prop(String) readonly parent!: string;
    @Prop({ default: false }) readonly cte: boolean;

    childItems: MaxValueItem[] = [];

    totalCount: number = 0;
    maxValue: number = 0;
    loading: boolean = false;
    pagination: Pager = {
        sortDesc: false,
        page: 1,
        rowsPerPage: 10,
        sortBy: "title",
        totalItems: 0,
        query: ""
    };

    headers: any = [
        { text: 'ItemId', value: 'itemId', sortable: false, align: 'left' },
        { text: 'Title', value: 'title', sortable: false, align: 'left' },
        { text: 'Value', value: 'value', sortable: false, align: 'left' },
    ];

    get myparams() {
        return {
            ...this.pagination,
            query: this.parent
        }
    }

    @Watch('myparams', { deep: true })
    async paramsChanged(val: any) {
      if (!this.loading)
        await this.loadData(val);
    }
    
    async loadData(options: Pager){
        try {
            this.loading = true;

            let optionsCopy = {...options};
            let optionsQuery = queryString.stringify(optionsCopy);

            let response = null;
            if (this.cte)
                response = await axios.get(`item/ctechilds?${optionsQuery}`);
            else
                response = await axios.get(`item/childs?${optionsQuery}`);

            this.childItems = response.data.items;
            this.totalCount = response.data.totalCount;
            this.maxValue = response.data.maxValue;
        } finally {
            this.loading = false;
        }
    }

    @Watch("cte")
    cteWatch(oldVal: any, newVal: any){
        let options = {
            ...this.pagination,
            query: this.parent
        };
        this.loadData(options);
    }

    async mounted() {
        
    }
}