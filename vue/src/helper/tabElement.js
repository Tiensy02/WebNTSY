/**
 * thực hiện chuyển đổi qua lại giữa các input element
 * @param {Array} elements mảng chứa các input element
 */
export default function tabElement(elements){
    let hasElementFocus = false
    for(let i = 0 ; i < elements.length ; i ++ ) {
            if(document.activeElement === elements[i] ) {
                    hasElementFocus=true
                    if( i == elements.length - 1 ) {
                        elements[0].focus()
                    }
                    else {
                        elements[i+1].focus()
                    }
                    break
        }
    }
    if(!hasElementFocus) {
        elements[0].focus()
    }
}