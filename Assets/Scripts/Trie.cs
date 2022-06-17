using System.Collections;
using System.Collections.Generic;
public class Trie
{
    public class TrieNode{
        public char c;
        public TrieNode[] children;
        public bool isWord;
        public TrieNode(char c){
            this.c = c;
            this.children = new TrieNode[26];

        }
    }
   public void insert(string product, TrieNode root){
        TrieNode node = root;
        for (int i = 0; i < product.Length; i++) {
            char c = product[i];
            if(node.children[c- 'a'] == null){
                node.children[c- 'a'] = new TrieNode(c);
            }
            node = node.children[c - 'a'];
        }
        node.isWord = true;
    }

    public TrieNode buildTrie(string[] products){
        TrieNode root = new TrieNode(' ');
        foreach(string product in products){
            insert(product, root);
        }
        // for (string product:
        //         products) {
        //     insert(product, root);
        // }
        return root;

    }
    private List<string> findTopThree(TrieNode root, string search){
        List<string> res = new List<string>();
        TrieNode node = root;
        foreach (char c in search.ToCharArray())
        {
           if(node.children[c - 'a'] == null){
                return res;
            }
            else{
                node = node.children[c - 'a'];
            } 
        }
        // for (char c: search.toCharArray()
        //      ) {
        //     if(node.children[c - 'a'] == null){
        //         return res;
        //     }
        //     else{
        //         node = node.children[c - 'a'];
        //     }
        // }
        if(node.isWord){
            res.Add(search);
        }
        foreach (TrieNode child in node.children)
        {
            if (child != null){
                List<string> thisRes = dfs(child, search, new List<string>());
                
                res.AddRange(thisRes);
                if(res.Count >= 3){
                    return res.GetRange(0,3);
                }
            }
        }
        // for (TrieNode child: node.children
        //      ) {
        //     if (child != null){
        //         List<String> thisRes = dfs(child, search, new ArrayList<>());
        //         res.addAll(thisRes);
        //         if(res.size() >= 3){
        //             return res.subList(0,3);
        //         }
        //     }
        // }
        return  res;
    }
    private List<string> dfs(TrieNode root, string word, List<string> res){
        if(root.isWord){
            res.Add(word + root.c);
            if(res.Count >= 3){
                return res;
            }
        }
        foreach (TrieNode child in root.children)
        {
            if(child != null){
                dfs(child, word + root.c, res);    
            }
        }
        // for (TrieNode child:
        //         root.children) {
        //     if(child != null){
        //         dfs(child, word + root.c, res);    
        //     }
            
        // }
        return res;
    }
   public List<List<string>> suggestedProducts(string[] products, string searchWord) {
         TrieNode root = buildTrie(products);
        List<List<string>> res = new List<List<string>>();
        for (int i = 1; i <= searchWord.Length; i++) {
            res.Add(findTopThree(root,searchWord.Substring(0,i)));
        }


        return res;
        
    }  
    
}