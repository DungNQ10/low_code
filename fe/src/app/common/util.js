export const SetValues = (valueObject, setValueFuc)=>{
    var keys = Object.keys(valueObject);
    keys.map(c=>{
        setValueFuc(c,valueObject[c]);
    });
} ;