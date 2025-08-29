/*
iven an array of strings strs, group the anagrams together. You can return the answer in any order.

 

Example 1:

Input: strs = ["eat","tea","tan","ate","nat","bat"]

Output: [["bat"],["nat","tan"],["ate","eat","tea"]]

Explanation:

There is no string in strs that can be rearranged to form "bat".
The strings "nat" and "tan" are anagrams as they can be rearranged to form each other.
The strings "ate", "eat", and "tea" are anagrams as they can be rearranged to form each other.
Example 2:

Input: strs = [""]

Output: [[""]]

Example 3:

Input: strs = ["a"]

Output: [["a"]]

 

Constraints:

1 <= strs.length <= 104
0 <= strs[i].length <= 100
strs[i] consists of lowercase English letters.

*/


var groupAnagrams = function(strs) {
    const sortedMap = new Map();

    strs.map(str => {
        sortedMap.set(str, str.split('').sort().join(''));
    })

    const resultMap = new Map();
    for(let [key, value] of sortedMap.entries()) {
        if(resultMap.has(value)) {
            resultMap.set(value, [...resultMap.get(value), key])
        }
        else {
            resultMap.set(value, [key])
        }
    }
    return [...resultMap.values()]
}


const samples = [["eat","tea","tan","ate","nat","bat"], [""], [], ["", ""]];
for(let sample of samples) {
    var result = groupAnagrams(sample);
    console.log(result);
}
