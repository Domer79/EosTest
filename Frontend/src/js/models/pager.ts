import { ParsedUrlQueryInput } from "querystring";

export default class Pager implements ParsedUrlQueryInput{
    [key: string]: any;
    page: number;
    rowsPerPage: number;
    sortBy: string;
    sortDesc: boolean;
    totalItems: number;
    query: string;
}