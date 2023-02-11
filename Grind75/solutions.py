# Given an array of integers nums and an integer target, 
# return indices of the two numbers such that they add up to target.
def twoSum(self, nums: List[int], target: int) -> List[int]:
        if len(nums) < 2:
            return -1
        diffs = {}
        for i in range(len(nums)):
            if target-nums[i] in diffs:
                return [diffs[target-nums[i]],i]
            else:
                diffs[nums[i]] = i
        return -1

# Given a string s containing just the characters '(', ')', '{', '}', '[' and ']', 
# determine if the input string is valid.
def isValid(self, s: str) -> bool:
    check = {')':'(','}':'{',']':'['}
    stack = []
    for i in range(len(s)):
        if s[i] in check: 
            if len(stack) <= 0 or stack.pop() != check[s[i]]:
                return False
        else:
            stack.append(s[i])
    if len(stack) > 0:
        return False
    return True

# You are given the heads of two sorted linked lists list1 and list2.
# Merge the two lists in a one sorted list. The list should be made by splicing together the nodes of the first two lists.
class ListNode:
    def __init__(self, val=0, next=None):
        self.val = val
        self.next = next

    def mergeTwoLists(self, list1: Optional[ListNode], list2: Optional[ListNode]) -> Optional[ListNode]:
        res = ListNode()
        head = res
        while list1 != None and list2 != None:
            if list1.val < list2.val:
                res.next = list1
                list1 = list1.next
            else:
                res.next = list2
                list2 = list2.next
            res = res.next
        res.next = list1 if list1 != None else list2
        return head.next
