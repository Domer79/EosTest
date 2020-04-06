import axios from 'src/js/axios';
import Vue from "vue";
import Component from "vue-class-component";
import { Model, Watch, Prop } from 'vue-property-decorator';
import queryString from 'querystring';
import Pager from 'src/js/models/pager';
import Item from 'src/js/models/item';

@Component({
    template: require('./template.html')
})
export default class ParentItemsComponent extends Vue {
    @Prop({ default: false }) readonly cte: boolean;

    @Prop()
    @Model('change', { type: Item }) readonly item: Item;
    parentItems: Item[] = [];
    totalCount: number = 0;
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

    change(item: Item){
        this.$emit('change', item);
    }

    itemSelected(ev: any){
        this.change(ev.item);
    }

    get myparams() {
        return {
            ...this.pagination,
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

            let response = await axios.get(`item/parents?${optionsQuery}`);
            this.parentItems = response.data.items;
            this.totalCount = response.data.totalCount;
        } finally {
            this.loading = false;
        }
    }

    @Watch("cte")
    async cteWatch(oldVal: any, newVal: any){
        await this.loadData(this.pagination);
    }

    async mounted() {
        await this.loadData(this.pagination);
    }
}