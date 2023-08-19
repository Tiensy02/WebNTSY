import BaseService from "./base-service";
class AwardService extends BaseService {
    constructor(){
        super("https://localhost:7166/api/v1/Award")
    }
}
export default AwardService;