using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Nodes.Properties;
using System;
using System.Collections.ObjectModel;

namespace OHM.Tests
{
    [TestClass]
    public class NodePropertyUnitTest
    {
        /// <summary>UT001 - Test Minimal Constructor with normal case
        ///  - Test passed value: (2 tests)
        ///     Key         {'key1' , 'key3'};
        ///     name        {'name1', 'name5'};
        ///     type        {Int32  , long};
        ///     isReadOnly  {true   , false};
        ///     description {null   , 'My description'};
        ///  - Test default value
        ///     Value must be Null
        /// </summary>
        [TestMethod]
        public void TestNodeProperty_Ctor_Minimal_UT001()
        {
            #region Test 1

            //Test values
            string key = "key1";
            string name = "name1";
            Type type = typeof(Int32);
            bool isReadOnly = true;
            string description = null;
            Int32 value = 0;

            //2. Run test
            NodeProperty target = new NodeProperty(key, name, type, isReadOnly, description, value);

            //3. Validate test
            //3.1  Check passed value
            Assert.AreEqual(key, target.Key);
            Assert.AreEqual(name, target.Name);
            Assert.AreEqual(type, target.Type);
            Assert.AreEqual(isReadOnly, target.ReadOnly);
            Assert.IsTrue(String.IsNullOrWhiteSpace(target.Description));

            //3.2 Check Default value
            Assert.AreEqual(value, target.Value);
            #endregion

            #region Test 2
            //Test values
            key = "key3";
            name = "name5";
            type = typeof(long);
            isReadOnly = false;
            description = "My description";
            long value2 = 0;

            //Run test
            target = new NodeProperty(key, name, type, isReadOnly, description, value2);

            //Check passed value
            Assert.AreEqual(key, target.Key);
            Assert.AreEqual(name, target.Name);
            Assert.AreEqual(type, target.Type);
            Assert.AreEqual(isReadOnly, target.ReadOnly);
            Assert.AreEqual(description, target.Description);

            //Check Default value
            Assert.AreEqual(value2, target.Value);
            #endregion
        }

        /// <summary>UT002 - Test Minimal Constructor with wrong key parameter
        ///          Must throw ArgumentNullException('key')
        /// Invalid parameter : (3 tests)
        ///  - key        {Null, "", "    "};
        /// </summary>
        /// <remarks>
        /// Invalid use case
        /// </remarks>
        [TestMethod]
        public void TestNodeProperty_Ctor_Minimal_UT002()
        {
            #region Init test

            //0. Set Constant value
            //   Not used in the current test.
            //   Only used to fill the argument required by the test
            string name = "name2";
            Type type = typeof(Int64);
            bool isReadOnly = false;
            string description = "";
            Int64 value = 0;

            #endregion

            #region First test with Null

            //1. Set test value
            string key = null;

            //2. Run test
            try
            {
                NodeProperty target = new NodeProperty(key, name, type, isReadOnly, description, value);
                //2.1 Fail test
                Assert.Fail("Must not allow Null as key argument");
            }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                //3. Validate test
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
                ArgumentNullException ex2 = ex as ArgumentNullException;
                Assert.AreEqual("key", ex2.ParamName);
            }

            #endregion

            #region Second test with string empty

            //1. Set test value
            key = "";

            //2. Run test
            try
            {
                NodeProperty target = new NodeProperty(key, name, type, isReadOnly, description, value);
                //2.1 Fail test
                Assert.Fail("Must not allow emptry string as key argument");
            }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                //3. Validate test
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
                ArgumentNullException ex2 = ex as ArgumentNullException;
                Assert.AreEqual("key", ex2.ParamName);
            }

            #endregion

            #region Third test with blank string ("     ")

            //1. Set test value
            key = "      ";

            //2. Run test
            try
            {
                NodeProperty target = new NodeProperty(key, name, type, isReadOnly, description, value);
                //2.1 Fail test
                Assert.Fail("Must not allow white space string as key argument");
            }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                //3. Validate test
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
                ArgumentNullException ex2 = ex as ArgumentNullException;
                Assert.AreEqual("key", ex2.ParamName);
            }

            #endregion
        }

        /// <summary>UT003 - Test Minimal Constructor with invalid name parameter
        ///         Must throw ArgumentNullException('name')
        /// Invalid parameter : (3 tests)
        ///  - name        {Null, "", "    "};
        /// </summary>
        /// <remarks>
        /// Invalid use case
        /// </remarks>
        [TestMethod]
        public void TestNodeProperty_Ctor_Minimal_UT003()
        {
            #region Init test

            //0. Set Constant value
            //   Not used in the current test.
            //   Only used to fill the argument required by the test
            string key = "goodKey";
            Type type = typeof(Int64);
            bool isReadOnly = false;
            string description = "";
            Int64 value = 0;

            #endregion

            #region Test 1 with null name

            //1. Set Test value
            string name = null;

            //Run test
            try
            {
                NodeProperty target = new NodeProperty(key, name, type, isReadOnly, description, value);
                Assert.Fail("Must not allow Null as name argument");
            }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
                ArgumentNullException ex2 = ex as ArgumentNullException;
                Assert.AreEqual("name", ex2.ParamName);
            }

            #endregion

            #region Test 2 with string empty

            //1. Set test value
            name = "";

            //2. Run test
            try
            {
                NodeProperty target = new NodeProperty(key, name, type, isReadOnly, description, value);
                //2.1 Fail test
                Assert.Fail("Must not allow emptry string as name argument");
            }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                //3. Validate test
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
                ArgumentNullException ex2 = ex as ArgumentNullException;
                Assert.AreEqual("name", ex2.ParamName);
            }

            #endregion

            #region Test 3 with blank string ("     ")

            //1. Set test value
            name = "      ";

            //2. Run test
            try
            {
                NodeProperty target = new NodeProperty(key, name, type, isReadOnly, description, value);
                //2.1 Fail test
                Assert.Fail("Must not allow white space string as name argument");
            }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                //3. Validate test
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
                ArgumentNullException ex2 = ex as ArgumentNullException;
                Assert.AreEqual("name", ex2.ParamName);
            }

            #endregion
        }

        /// <summary>UT004 - Test Minimal Constructor with invalid value parameter
        /// 
        /// Invalid paramter : (1 tests)
        ///  - value  (Wrong type)
        /// </summary>
        /// <remarks>
        /// Invalid use cas
        /// </remarks>
        [TestMethod]
        public void TestNodeProperty_Ctor_Minimal_UT004()
        {
            #region Init test

            //0. Set Constant value
            //   Not used in the current test.
            //   Only used to fill the argument required by the test
            string key = "key1";
            string name = "name1";
            string description = "Description";
            #endregion

            #region Test 1 (Wrong value type)

                //1. Set test value
                //Wrong type for the value
                Type type = typeof(Int32);
                string value = "bla";

                //2. Run test
                try
                {
                    NodeProperty target = new NodeProperty(key, name, type, false, description, value);
                    //2.1 Fail test
                    Assert.Fail("Must not allow different type");
                }
                catch (AssertFailedException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    //3. Validate test
                    Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
                    ArgumentOutOfRangeException ex2 = ex as ArgumentOutOfRangeException;
                    Assert.AreEqual("value", ex2.ParamName);
                    Assert.AreEqual(value, ex2.ActualValue);
                }                
            #endregion

            /*#region Test 2 (Not nullable value)

                //1. Set test value
                //Wrong type for the value
                type = typeof(Int32);
                object value2 = null;

                //2. Run test
                try
                {
                    NodeProperty target = new NodeProperty(key, name, type, false, description, value2);
                    //2.1 Fail test
                    Assert.Fail("Must not allow null value when not declared as nullable");
                }
                catch (AssertFailedException ex)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    //3. Validate test
                    Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
                    ArgumentOutOfRangeException ex2 = ex as ArgumentOutOfRangeException;
                    Assert.AreEqual("value", ex2.ParamName);
                    Assert.AreEqual(value2, ex2.ActualValue);
                }
            #endregion*/
        }

        [TestMethod]
        public void TestNodeProperty_Ctor_Full_UT005()
        {
            //0. Set Constant value
            //   Not used in the current test.
            //   Only used to fill the argument required by the test

            //1. Set test value
            string key = "key34";
            string name = "name34";
            string description = "Description";
            Int32 value = 67;
            Type type = value.GetType();

            //Extra info
            ObservableCollection<INodeProperty> extraInfo = new ObservableCollection<INodeProperty>();
            extraInfo.Add(new NodeProperty("extra1", "extra1Name", typeof(string), true, "", null));

            //2. Run test
            NodeProperty target = new NodeProperty(key, name, type, false, description, value, extraInfo);

            //3. Validate test
            Assert.AreEqual(key, target.Key);                           //Test key
            Assert.AreEqual(name, target.Name);                         //Test name
            Assert.AreEqual(type, target.Type);                         //Test type
            Assert.IsFalse(target.ReadOnly);                            //Test read only
            Assert.AreEqual(description, target.Description);           //Test description
            Assert.AreEqual(value, target.Value);                       //Test value
            Assert.AreEqual(extraInfo.Count, target.Properties.Count);  //Test extra properties count

            //TODO
            Assert.Fail("Missing test");
        }

        //TODO TO VALIDATE AND RENAME
        #region Test Notify Property Changed with value

        // Boolean state for the event trigger
        private bool _CheckPropertyChangedForTestNodePropertySetValueWithNull = false;

        [TestMethod]
        public void TestNodeProperty_NotifyPropertyChanged()
        {
            string key = "key1";
            string name = "name1";
            Type type = typeof(Int32);
            Int32 value = 0;
            NodeProperty target = new NodeProperty(key, name, type, true, "", value);
            target.PropertyChanged += target_PropertyChanged;

            _CheckPropertyChangedForTestNodePropertySetValueWithNull = false;

            bool result = target.SetValue(null);

            Assert.IsTrue(result);
            Assert.IsTrue(_CheckPropertyChangedForTestNodePropertySetValueWithNull);
        }

        void target_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _CheckPropertyChangedForTestNodePropertySetValueWithNull = true;
        }

        #endregion
    }
}
