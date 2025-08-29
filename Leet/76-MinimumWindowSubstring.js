/**
 * 76. Minimum Window Substring
 * Given two strings s and t of lengths m and n respectively, return the minimum window substring of s such that every character in t (including duplicates) is included in the window. If there is no such substring, return the empty string "".

The testcases will be generated such that the answer is unique.

 

Example 1:

Input: s = "ADOBECODEBANC", t = "ABC"
Output: "BANC"
Explanation: The minimum window substring "BANC" includes 'A', 'B', and 'C' from string t.
Example 2:

Input: s = "a", t = "a"
Output: "a"
Explanation: The entire string s is the minimum window.
Example 3:

Input: s = "a", t = "aa"
Output: ""
Explanation: Both 'a's from t must be included in the window.
Since the largest window of s only has one 'a', return empty string.
 

Constraints:

m == s.length
n == t.length
1 <= m, n <= 105
s and t consist of uppercase and lowercase English letters.
*/

var minWindow = function(s, t) {

    let leftIndex = 0;
    const mapHolder = new Map();

    for(let rightIndex = 0; rightIndex < s.length; rightIndex++) {

        const selectedChars = s.substring(leftIndex, rightIndex + 1)
        if(mapHolder.has(selectedChars)){
            leftIndex = rightIndex + 1;
        }

        let selectedCharsIncludeT = false;

        let counter = 0;
        selectedChars.split('').map(_s => {
            t.split('').map(_t => {
                if(_s === _t) {
                    counter++;
                }
            })
            if(counter == t.length) selectedCharsIncludeT = true;
        })

        if(selectedCharsIncludeT) mapHolder.set(selectedChars, rightIndex - leftIndex + 1)

    }
    
    return {s, t}
};

const samples = [
    ["ADOBECODEBANC", "ABC"],
    ["a", "a"],
    ["a", "aa"]
];
 for(const sample of samples) {
    const result = minWindow(...sample);
    console.log(`${sample} => ${JSON.stringify(result)}`);
 }