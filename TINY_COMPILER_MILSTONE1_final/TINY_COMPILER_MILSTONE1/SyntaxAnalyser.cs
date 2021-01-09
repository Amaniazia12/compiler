using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TINY_COMPILER_MILSTONE1;

namespace JASONParser
{
    public class Node
    {
        public List<Node> children = new List<Node>();
        public string Name;
        public Node(string Name)
        {
            this.Name = Name;
        }
    }
   public class SyntaxAnalyser
    {

        int tokenIndex = 0;
        static List<TINY_Token> TokenStream;
        public static Node root;

        public  Node Parse(List<TINY_Token> Tokens)
        {
            TokenStream = Tokens;
                 root = Program();
           
            return root;
        }
        public Node Condition()
        {
            Node node = new Node("Condition");
            Node identifier = Identifier();
            Node condition_operator = Condition_operator();
            Node term = Term();
            if (identifier != null && condition_operator != null && term != null)
            {
                node.children.Add(identifier);
                node.children.Add(condition_operator);
                node.children.Add(term);
                return node;
            }
             return null;
        }
        public Node Term()
        {
            return null;
        }
        public Node Condition_operator()
        {
            Node node = new Node("Condition_operator");
            if (TokenStream[tokenIndex].token_type == TINY_Token_Class.GreaterThanOp)
            {
                node.children.Add(match(TINY_Token_Class.GreaterThanOp));
                return node;
            }
            else if (TokenStream[tokenIndex].token_type == TINY_Token_Class.LessThanOp)
            {
                node.children.Add(match(TINY_Token_Class.LessThanOp));
                return node;
            }
            else if (TokenStream[tokenIndex].token_type == TINY_Token_Class.ISEqualOp)
            {
                node.children.Add(match(TINY_Token_Class.ISEqualOp));
                return node;
            }
            else if (TokenStream[tokenIndex].token_type == TINY_Token_Class.NotEqualOp)
            {
                node.children.Add(match(TINY_Token_Class.NotEqualOp));
                return node;
            }

            return null;
        }
        public Node Return_statement()
        {
            Node node = new Node("Return_statement");
            Node Return = match(TINY_Token_Class.Return);
            Node expression = Expression();
            Node semicolon = match(TINY_Token_Class.Semicolon);
            if(Return!=null && expression!=null && semicolon != null)
            {
                node.children.Add(Return);
                node.children.Add(expression);
                node.children.Add(semicolon);
            }
            return null;
        }
        public Node ReadStatement()
        {
            Node node = new Node("ReadStatement");
            Node read = match(TINY_Token_Class.read);
            Node identifier = Identifier();
            Node semicolon = match(TINY_Token_Class.Semicolon);
            if (read != null && identifier != null && semicolon != null)
            {
                node.children.Add(read);
                node.children.Add(identifier);
                node.children.Add(semicolon);
                return node;
            }
            return null;
        }
        public Node WriteStatement()
        {
            Node node = new Node("WriteStatement");
            Node write = match(TINY_Token_Class.write);
            Node expression = Expression();
            Node endl = match(TINY_Token_Class.endl);
            Node semicolon = match(TINY_Token_Class.Semicolon);
            if (write != null && expression != null && endl != null && semicolon != null)
            {
                node.children.Add(write);
                node.children.Add(expression);
                node.children.Add(endl);
                node.children.Add(semicolon);
                return node;
            }
            return null;
        }
        public Node Expression()
        {
            return null;
        }
        public Node Identifier()
        {
            return null;
        }
        public Node Program()
        {
            return null;
        }
        public Node DeclarationStatement()
        {
            return null;
        }

        public Node Function_Call()
        {
            return null;
        }

        public Node match(TINY_Token_Class ExpectedToken)
        {
            return null;
        }
        // && or ||
        public Node Boolean_Operator()
        {
            Node node = new Node("Boolean_Operator");
            if (match(TINY_Token_Class.AndOp) != null)
            {
                node.children.Add(match(TINY_Token_Class.AndOp));
                return node;
            }
            else if (match(TINY_Token_Class.OROp) != null) 
            { 
                node.children.Add(match(TINY_Token_Class.OROp));
                return node;
            }
            return null;
        }
        //if (a+b>5)
        //if (a==1 && r !=0 || y==g)
        public Node Condition_Statement()
        {
            Node node = new Node("condition_statement"); 
            if (Condition() != null)
            {   
                if (Boolean_Operator() != null)
                {
                  if(Condition_Statement() != null)
                    {
                      node.children.Add(Condition());
                      node.children.Add(Boolean_Operator());
                      node.children.Add(Condition_Statement());
                      return node;
                    }
                }
                if (Condition() != null)   // fe hena note
                {
                    node.children.Add(Condition());
                    return node;
                }
            } 
            return null;
        }
         public Node if_statement()
        {
            Node node = new Node("if_statement");
            
            if (match(TINY_Token_Class.If) !=null )
            {
                if (Condition_Statement() != null)
                {
                    if (match(TINY_Token_Class.then) != null) 
                    {
                        if (Statements() != null)
                        {   
                            if (Else_Claose() != null)
                            {
                                node.children.Add(match(TINY_Token_Class.If));
                                node.children.Add(Condition_Statement());
                                node.children.Add(match(TINY_Token_Class.then));
                                node.children.Add(Statements());
                                node.children.Add(Else_Claose());
                                return node;

                            }
                        }
                    }   
                }
            }

            return null;
        }

        public Node ELse_if_statement()
        {
            Node node = new Node("Else_if_statement");
            
            if (match(TINY_Token_Class.Elseif) != null)
            {
                if (Condition_Statement() != null)
                {
                    if (match(TINY_Token_Class.then) != null)
                    {
                        if (Statements() != null)
                        {
                            if (Else_Claose() != null)
                            {
                                node.children.Add(match(TINY_Token_Class.Elseif));
                                node.children.Add(Condition_Statement());
                                node.children.Add(match(TINY_Token_Class.then));
                                node.children.Add(Statements());
                                node.children.Add(Else_Claose());
                                return node;

                            }
                        }
                    }
                }
            }

            return null;
        }
        public Node Else_statement()
        {
            Node node = new Node("if_statement");
            
            if (match(TINY_Token_Class.Else) != null)
            {
                if (Statements() != null)
                {
                    if (match(TINY_Token_Class.endl) != null)
                    {
                        node.children.Add(match(TINY_Token_Class.Else));
                        node.children.Add(Statements());
                        node.children.Add(match(TINY_Token_Class.endl));
                        return node;

                    }
                }
            }

            return null;
        }

        public Node Statements()
        {
            return null;
        }
        public Node Else_Claose()
        {
            Node node = new Node("if_statement");
            if (ELse_if_statement() != null)
            {
                node.children.Add(ELse_if_statement());
                return node;
            }
            else if (Else_statement() != null)
            {
                node.children.Add(Else_statement());
                return node;
            }
            else if (match(TINY_Token_Class.endl) != null)
            {
                node.children.Add(match(TINY_Token_Class.endl));
                return node;
            }
            return null;
        }
        

        //use this function to print the parse tree in TreeView Toolbox
        public static TreeNode PrintParseTree(Node root)
        {
            TreeNode tree = new TreeNode("Parse Tree");
            TreeNode treeRoot = PrintTree(root);
            if (treeRoot != null)
                tree.Nodes.Add(treeRoot);
            return tree;
        }
        static TreeNode PrintTree(Node root)
        {
            if (root == null || root.Name == null)
                return null;
            TreeNode tree = new TreeNode(root.Name);
            if (root.children.Count == 0)
                return tree;
            foreach (Node child in root.children)
            {
                if (child == null)
                    continue;
                tree.Nodes.Add(PrintTree(child));
            }
            return tree;
        }
    }
}
