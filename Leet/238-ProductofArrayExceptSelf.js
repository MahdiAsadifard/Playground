/**
 * Given an integer array nums, return an array answer such that answer[i] is equal to the product of all the elements of nums except nums[i].

The product of any prefix or suffix of nums is guaranteed to fit in a 32-bit integer.

You must write an algorithm that runs in O(n) time and without using the division operation.

 

Example 1:

Input: nums = [1,2,3,4]
Output: [24,12,8,6]
Example 2:

Input: nums = [-1,1,0,-3,3]
Output: [0,0,9,0,0]
 

Constraints:

2 <= nums.length <= 105
-30 <= nums[i] <= 30
The input is generated such that answer[i] is guaranteed to fit in a 32-bit integer.
 

Follow up: Can you solve the problem in O(1) extra space complexity? (The output array does not count as extra space for space complexity analysis.)
 */

var productExceptSelf = function(nums) {
    const left = [];
    nums.map((num, index) => {
        if(index == 0) left.push(1);
        else if(index == 1) left.push(nums[0]);
        else left.push(left[index-1] * nums[index - 1])
    })
    const right = [];
    for(let i = nums.length - 1; i >= 0; i--){

        if(i == nums.length -1 ) right.push(1);
        else if(i == nums.length -2) right.push(nums[i + 1]);
        else {
            const n = nums[i + 1];
            const r = right[right.length - 1];
            right.push(n * r);
        }

    }
    right.reverse();
    const result = left.map((l,i) => l * right[i]);
    return result;
};

const samples = [[1,2,3,4], [-1,1,0,-3,3]];
for (const sample of samples) {
    console.log(`${sample}: `, productExceptSelf(sample));
}